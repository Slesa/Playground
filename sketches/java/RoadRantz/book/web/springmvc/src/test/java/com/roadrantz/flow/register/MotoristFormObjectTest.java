package com.roadrantz.flow.register;

import static java.util.Collections.*;
import static org.junit.Assert.*;

import java.util.Arrays;
import java.util.List;

import org.junit.Test;

import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Vehicle;

public class MotoristFormObjectTest {
   @Test
   public void shouldReturnAMotoristWithABlankVehicle() {
      MotoristFormObject motoristForm = new MotoristFormObject();
      Motorist motorist = motoristForm.getMotorist();

      assertNotNull(motorist);
      assertEquals(1, motorist.getVehicles().size());
   }

   @Test
   public void shouldReturnAnEmptyVehicleList() {
      MotoristFormObject motoristForm = new MotoristFormObject();
      assertEquals(emptyList(), motoristForm.getScrubbedVehicles());
   }

   @Test
   public void shouldReturnAScrubbedVehicleList() {
      MotoristFormObject motoristForm = new MotoristFormObject();
      Motorist motorist = motoristForm.getMotorist();
      List<Vehicle> vehicles = motorist.getVehicles();
      vehicles.get(0).setState("TX");
      vehicles.get(0).setPlateNumber("ABC123");
      vehicles.add(new Vehicle());
      vehicles.get(1).setState("NM");
      vehicles.get(1).setPlateNumber("XYZ321");
      vehicles.add(new Vehicle());

      assertEquals(Arrays.asList(vehicles.get(0), vehicles.get(1)),
                        motoristForm.getScrubbedVehicles());
   }

   @Test
   public void shouldReturnNextVehicleInList() {
      MotoristFormObject motoristForm = new MotoristFormObject();
      Motorist motorist = motoristForm.getMotorist();
      List<Vehicle> vehicles = motorist.getVehicles();
      vehicles.get(0).setState("TX");
      vehicles.get(0).setPlateNumber("ABC123");
      vehicles.add(new Vehicle());
      vehicles.get(1).setState("NM");
      vehicles.get(1).setPlateNumber("XYZ321");
      vehicles.add(new Vehicle());

      assertEquals(vehicles.get(2), motoristForm.getNextVehicle());
   }
}
