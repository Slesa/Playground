package com.roadrantz.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.apache.log4j.Logger;
import org.joda.time.LocalDate;
import org.springframework.transaction.annotation.Isolation;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

import com.roadrantz.dao.RantDao;
import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.UserPrivilege;
import com.roadrantz.domain.Vehicle;

@Transactional(propagation = Propagation.SUPPORTS, readOnly = true)
public class RantServiceImpl implements RantService {

   private static final Logger LOGGER = Logger.getLogger(RantServiceImpl.class);

   /**
    * The addRant() method, as originally described in Listing 6.1. Eventually
    * evolved to use the Transactional annotation in Listing 6.4.
    */
   @Transactional(propagation = Propagation.REQUIRED)
   public void addRant(Rant rant) {
      rant.setPostedDate(new Date());

      Vehicle rantVehicle = rant.getVehicle();

      // check for existing vehicle with same plates
      Vehicle existingVehicle = rantDao.findVehicleByPlate(rantVehicle
                        .getState(), rantVehicle.getPlateNumber());

      if (existingVehicle != null) {
         // associate existing vehicle with rant
         rant.setVehicle(existingVehicle);
      } else {
         rantDao.saveVehicle(rantVehicle);
      }

      rantDao.saveRant(rant);
   }

   public List<Rant> getRecentRants() {
      return rantDao.getAllRants();
   }

   @Transactional(propagation = Propagation.REQUIRED)
   public void addMotorist(Motorist motorist)
                     throws MotoristAlreadyExistsException {
      Motorist existingMotorist = rantDao.getMotoristByEmail(motorist
                        .getUsername());
      if (existingMotorist != null) {
         throw new MotoristAlreadyExistsException();
      }

      UserPrivilege privilege = new UserPrivilege("ROLE_MOTORIST");
      privilege.setUsername(motorist.getUsername());
      privilege.setUser(motorist);
      motorist.getPrivileges().add(privilege);

      List<Vehicle> vehicles = new ArrayList<Vehicle>();
      for (Vehicle vehicle : motorist.getVehicles()) {
         LOGGER.debug("Checking for existing vehicle: " + vehicle.getState()
                           + " :: " + vehicle.getPlateNumber());

         Vehicle existingVehicle = rantDao.findVehicleByPlate(vehicle
                           .getState(), vehicle.getPlateNumber());

         if (existingVehicle == null) {
            rantDao.saveVehicle(vehicle);
            vehicle.setMotorist(motorist);
            vehicles.add(vehicle);
         } else {
            LOGGER.debug("Found existing vehicle: " + vehicle.getState()
                              + " :: " + vehicle.getPlateNumber());
            existingVehicle.setMotorist(motorist);
            vehicles.add(existingVehicle);
         }
      }

      motorist.setVehicles(vehicles);
      rantDao.saveMotorist(motorist);
   }

   @Transactional(propagation = Propagation.SUPPORTS, isolation = Isolation.READ_UNCOMMITTED, readOnly = true)
   public List<Rant> getRantsForVehicle(Vehicle requestedVehicle) {
      Vehicle vehicle = rantDao.findVehicleByPlate(requestedVehicle.getState(),
                        requestedVehicle.getPlateNumber());

      return vehicle.getRants();
   }

   public Motorist getMotoristByEmail(String email) {
      return rantDao.getMotoristByEmail(email);
   }

   public List<Rant> getRantsForMotorist(String email) {
      List<Rant> motoristRants = new ArrayList<Rant>();

      Motorist motorist = getMotoristByEmail(email);
      for (Vehicle vehicle : motorist.getVehicles()) {
         motoristRants.addAll(vehicle.getRants());
      }

      return motoristRants;
   }

   public List<Rant> getRantsForDay(LocalDate day) {
      return rantDao.getRantsForDay(day);
   }

   // injected
   private RantDao rantDao;

   public void setRantDao(RantDao rantDao) {
      this.rantDao = rantDao;
   }

}
