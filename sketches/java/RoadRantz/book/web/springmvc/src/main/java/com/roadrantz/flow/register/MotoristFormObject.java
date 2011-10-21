package com.roadrantz.flow.register;

import java.io.Serializable;
import java.util.Collections;
import java.util.List;

import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Vehicle;

@SuppressWarnings("serial")
public class MotoristFormObject implements Serializable {
   private final Motorist motorist;

   public MotoristFormObject() {
      motorist = new Motorist();
      motorist.getVehicles().add(new Vehicle()); // prime the set of vehicles
   }

   public Motorist getMotorist() {
      return motorist;
   }

   public List<Vehicle> getScrubbedVehicles() {
      List<Vehicle> vehicles = motorist.getVehicles();
      if (vehicles.size() > 1) {
         return vehicles.subList(0, vehicles.size() - 1);
      }

      return Collections.emptyList();
   }

   public Vehicle getNextVehicle() {
      return motorist.getVehicles().get(motorist.getVehicles().size() - 1);
   }
}
