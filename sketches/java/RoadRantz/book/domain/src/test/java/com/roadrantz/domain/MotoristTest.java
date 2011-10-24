package com.roadrantz.domain;

import static java.util.Arrays.*;
import static org.junit.Assert.*;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.junit.Before;
import org.junit.Test;

public class MotoristTest {
   private Motorist motorist;
   private Set<UserPrivilege> expectedPrivileges;
   private List<Vehicle> expectedVehicles;

   @Before
   public void setup() {
      motorist = new Motorist();
      motorist.setUsername("email");
      motorist.setFirstName("first name");
      motorist.setLastName("last name");
      motorist.setPassword("password");

      UserPrivilege[] privileges = new UserPrivilege[] {
            new UserPrivilege("privilege 1"),
            new UserPrivilege("privilege 2"),
            new UserPrivilege("privilege 3") };
      expectedPrivileges = new HashSet<UserPrivilege>(asList(privileges));
      motorist.setPrivileges(expectedPrivileges);

      Vehicle vehicle1 = new Vehicle();
      vehicle1.setId(111);
      vehicle1.setPlateNumber("AAA111");
      vehicle1.setState("AL");
      vehicle1.setRants(asList(new Rant()));
      Vehicle vehicle2 = new Vehicle();
      vehicle2.setId(222);
      vehicle2.setPlateNumber("BBB222");
      vehicle2.setState("AR");
      vehicle2.setRants(asList(new Rant(), new Rant()));
      Vehicle vehicle3 = new Vehicle();
      vehicle3.setId(333);
      vehicle3.setPlateNumber("CCC333");
      vehicle3.setState("CA");
      vehicle3.setRants(asList(new Rant(), new Rant(), new Rant()));
      expectedVehicles = asList(vehicle1, vehicle2, vehicle3);
      motorist.setVehicles(expectedVehicles);
   }

   @Test
   public void testAllSimpleSettersAndGetters() {
      assertEquals("email", motorist.getUsername());
      assertEquals("first name", motorist.getFirstName());
      assertEquals("last name", motorist.getLastName());
      assertEquals("password", motorist.getPassword());

      assertTrue(expectedPrivileges.containsAll(motorist.getPrivileges()));
      assertTrue(motorist.getPrivileges().containsAll(expectedPrivileges));
   }

   @Test
   public void testVehicleAndRantSettersAndGetters() {
      assertEquals(6, motorist.getRants().size());
      assertEquals(expectedVehicles, motorist.getVehicles());
   }

   @Test
   public void testToString() {
      String toString = motorist.toString();
      assertNotNull(toString);
      assertTrue(toString.startsWith(Motorist.class.getName()));
      assertTrue(toString.contains("firstName=first name"));
      assertTrue(toString.contains("lastName=last name"));
      assertTrue(toString.contains("username=email"));
      assertTrue(toString.contains("password=password"));
   }
}
