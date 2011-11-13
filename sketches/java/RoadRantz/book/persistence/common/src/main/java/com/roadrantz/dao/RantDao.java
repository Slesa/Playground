package com.roadrantz.dao;

import java.util.List;

import org.joda.time.LocalDate;

import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;

public interface RantDao {
   public void saveRant(Rant rant);

   public List<Rant> getAllRants();

   public List<Rant> getRantsForDay(LocalDate day);

   public Vehicle findVehicleByPlate(String state, String plateNumber);

   public void saveVehicle(Vehicle vehicle);

   public Motorist getMotoristByEmail(String email);

   public void saveMotorist(Motorist driver);
}
