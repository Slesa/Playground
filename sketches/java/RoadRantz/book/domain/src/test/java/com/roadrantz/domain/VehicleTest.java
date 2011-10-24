package com.roadrantz.domain;

import static java.util.Arrays.*;
import static org.junit.Assert.*;

import java.util.List;

import org.junit.Test;

public class VehicleTest {
   @Test
   public void simpleSetterAndGettersShouldWork() {
      Vehicle vehicle = new Vehicle();
      vehicle.setId(321);
      Motorist expectedMotorist = new Motorist();
      vehicle.setMotorist(expectedMotorist);
      vehicle.setPlateNumber("ABC321");
      vehicle.setState("TX");
      List<Rant> expectedRants = asList(new Rant(), new Rant(), new Rant());
      vehicle.setRants(expectedRants);
      
      assertEquals(new Integer(321), vehicle.getId());
      assertEquals("ABC321", vehicle.getPlateNumber());
      assertEquals("TX", vehicle.getState());
      assertEquals(expectedMotorist, vehicle.getMotorist());
      assertEquals(expectedRants, vehicle.getRants());
   }
   
   @Test
   public void shouldStripNonAlphanumericFromPlateNumber() {
      Vehicle vehicle = new Vehicle();
      vehicle.setPlateNumber(" 1.2/3&A$B!C'");
      
      assertEquals("123ABC", vehicle.getPlateNumber());
   }
   
   @Test
   public void shouldKeepNullIfPlateNumberIsSetToNull() {
      Vehicle vehicle = new Vehicle();
      vehicle.setPlateNumber(null);
      assertNull(vehicle.getPlateNumber());
   }
   
   @Test
   public void twoDistinctButSimilarVehiclesShouldBeEqual() {
      Vehicle vehicle1 = new Vehicle();
      vehicle1.setId(246);
      vehicle1.setState("TX");
      vehicle1.setPlateNumber("CBA123");

      Vehicle vehicle2 = new Vehicle();
      vehicle2.setId(246);
      vehicle2.setState("TX");
      vehicle2.setPlateNumber("CBA123");

      assertEquals(vehicle1, vehicle2);
   }
   
   @Test
   public void twoDifferentVehiclesShouldNotBeEqual() {
      Vehicle vehicle1 = new Vehicle();
      vehicle1.setId(135);
      vehicle1.setState("AK");
      vehicle1.setPlateNumber("XYZ987");

      Vehicle vehicle2 = new Vehicle();
      vehicle2.setId(246);
      vehicle2.setState("TX");
      vehicle2.setPlateNumber("CBA123");

      assertFalse(vehicle1.equals(vehicle2));
   }
}
