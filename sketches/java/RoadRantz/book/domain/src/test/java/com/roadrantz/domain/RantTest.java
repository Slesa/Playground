package com.roadrantz.domain;

import static org.junit.Assert.*;

import java.util.Date;

import org.junit.Test;

public class RantTest {
   @Test
   public void testSimpleSettersAndGetters() {
      Rant rant = new Rant();
      rant.setId(987);
      Date expectedDate = new Date();
      rant.setPostedDate(expectedDate);
      rant.setRantText("rant text");
      Vehicle expectedVehicle = new Vehicle();
      rant.setVehicle(expectedVehicle);
      
      assertEquals(new Integer(987), rant.getId());
      assertEquals(expectedDate, rant.getPostedDate());
      assertEquals("rant text", rant.getRantText());
      assertEquals(expectedVehicle, rant.getVehicle());
   }
   
   @Test
   public void rantsWithSameIdShouldBeEqual() {
      Rant rant1 = new Rant();
      rant1.setId(213);
      Rant rant2 = new Rant();
      rant2.setId(213);
      
      assertEquals(rant1, rant2);
   }
   
   @Test
   public void rantsWithDifferentIdShouldNotBeEqual() {
      Rant rant1 = new Rant();
      rant1.setId(213);
      Rant rant2 = new Rant();
      rant2.setId(312);
      
      assertFalse(rant1.equals(rant2));
   }
}
