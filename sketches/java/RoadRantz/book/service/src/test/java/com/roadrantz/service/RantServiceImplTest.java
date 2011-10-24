package com.roadrantz.service;

import static java.util.Arrays.*;
import static org.easymock.EasyMock.*;
import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.List;

import org.joda.time.LocalDate;
import org.junit.Before;
import org.junit.Test;

import com.roadrantz.dao.RantDao;
import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;

public class RantServiceImplTest {
   private RantServiceImpl rantService;
   private RantDao fakeRantDao;

   @Before
   public void setup() {
      rantService = new RantServiceImpl();

      // fakeRantDao = new FakeRantDao();
      fakeRantDao = createNiceMock(RantDao.class);
      rantService.setRantDao(fakeRantDao);
   }

   @Test
   public void shouldSuccessfullyAddMotoristWithNonExistantVehicle()
                     throws Exception {

      Vehicle vehicle = new Vehicle();
      vehicle.setState("TX");
      vehicle.setPlateNumber("123ABC");

      Motorist motorist = new Motorist();
      motorist.setFirstName("Bob");
      motorist.setLastName("McSmith");
      motorist.setUsername("bob@mcsmith.com");
      motorist.setPassword("letmein");
      ArrayList<Vehicle> vehicles = new ArrayList<Vehicle>();
      vehicles.addAll(asList(vehicle));
      motorist.setVehicles(vehicles);

      fakeRantDao.findVehicleByPlate("TX", "123ABC");
      expectLastCall().andReturn(null);
      replay(fakeRantDao);

      assertEquals(0, motorist.getPrivileges().size());
      assertNull(vehicle.getMotorist());
      rantService.addMotorist(motorist);
      assertEquals(1, motorist.getPrivileges().size());
      assertSame(motorist, vehicle.getMotorist());
   }

   @Test
   public void shouldSuccessfullyAddMotoristWithExistantVehicle()
                     throws Exception {

      Vehicle expectedVehicle = new Vehicle();
      expectedVehicle.setState("TX");
      expectedVehicle.setPlateNumber("123ABC");

      Vehicle vehicle = new Vehicle();
      vehicle.setState("TX");
      vehicle.setPlateNumber("123ABC");

      Motorist motorist = new Motorist();
      motorist.setFirstName("Bob");
      motorist.setLastName("McSmith");
      motorist.setUsername("bob@mcsmith.com");
      motorist.setPassword("letmein");
      ArrayList<Vehicle> vehicles = new ArrayList<Vehicle>();
      vehicles.addAll(asList(vehicle));
      motorist.setVehicles(vehicles);

      fakeRantDao.findVehicleByPlate("TX", "123ABC");
      expectLastCall().andReturn(expectedVehicle);
      replay(fakeRantDao);

      assertEquals(0, motorist.getPrivileges().size());
      assertNull(expectedVehicle.getMotorist());
      rantService.addMotorist(motorist);
      assertEquals(1, motorist.getPrivileges().size());
      assertSame(motorist, expectedVehicle.getMotorist());
      assertTrue(motorist.getVehicles().contains(expectedVehicle));
   }

   @Test(expected = MotoristAlreadyExistsException.class)
   public void shouldThrowExceptionIfMotoristWithSameEmailAlreadyExists()
                     throws Exception {
      Motorist motorist = new Motorist();
      motorist.setUsername("already@exist.com");

      fakeRantDao.getMotoristByEmail("already@exist.com");
      expectLastCall().andReturn(motorist);
      replay(fakeRantDao);

      rantService.addMotorist(motorist);
   }

   @Test
   public void shouldReturnRantsForDayAsPassThruFromDao() {
      LocalDate day = new LocalDate();
      fakeRantDao.getRantsForDay(day);

      Rant rant1 = new Rant();
      rant1.setRantText("rant1");
      Rant rant2 = new Rant();
      rant2.setRantText("rant2");
      Rant rant3 = new Rant();
      rant3.setRantText("rant3");

      expectLastCall().andReturn(asList(rant1, rant2, rant3));
      replay(fakeRantDao);

      List<Rant> rants = rantService.getRantsForDay(day);

      assertEquals(3, rants.size());
      assertEquals("rant1", rants.get(0).getRantText());
      assertEquals("rant2", rants.get(1).getRantText());
      assertEquals("rant3", rants.get(2).getRantText());
   }

   @Test
   public void shouldReturnRantsForVehicleAsPassThruFromDao() {
      Vehicle vehicle = new Vehicle();
      vehicle.setState("TX");
      vehicle.setPlateNumber("J55DNY");
      Rant rant1 = new Rant();
      rant1.setRantText("rant A");
      Rant rant2 = new Rant();
      rant2.setRantText("rant B");
      vehicle.setRants(asList(rant1, rant2));

      fakeRantDao.findVehicleByPlate("TX", "J55DNY");
      expectLastCall().andReturn(vehicle);
      replay(fakeRantDao);

      List<Rant> rants = rantService.getRantsForVehicle(vehicle);
      assertEquals(2, rants.size());
      assertEquals("rant A", rants.get(0).getRantText());
      assertEquals("rant B", rants.get(1).getRantText());
   }

   @Test
   public void shouldAddRantToExistingVehicle() {
      Vehicle vehicle = new Vehicle();
      vehicle.setState("TX");
      vehicle.setPlateNumber("J55DNY");

      Vehicle expectedVehicle = new Vehicle();
      expectedVehicle.setState("TX");
      expectedVehicle.setPlateNumber("J55DNY");

      fakeRantDao.findVehicleByPlate("TX", "J55DNY");
      expectLastCall().andReturn(expectedVehicle);
      replay(fakeRantDao);

      Rant rant = new Rant();
      rant.setRantText("rant X");
      rant.setVehicle(vehicle);

      assertNull(rant.getId());
      assertNull(rant.getPostedDate());
      rantService.addRant(rant);
      assertNotNull(rant.getPostedDate());
      assertNotSame(vehicle, rant.getVehicle());
   }

   @Test
   public void shouldAddRantToNonExistantVehicle() {
      Vehicle vehicle = new Vehicle();
      vehicle.setState("TX");
      vehicle.setPlateNumber("333CCC");

      fakeRantDao.findVehicleByPlate("TX", "333CCC");
      expectLastCall().andReturn(null);
      replay(fakeRantDao);

      Rant rant = new Rant();
      rant.setRantText("rant X");
      rant.setVehicle(vehicle);

      assertNull(rant.getId());
      assertNull(rant.getPostedDate());
      assertNull(vehicle.getId());
      rantService.addRant(rant);
      assertNotNull(rant.getPostedDate());
      assertSame(vehicle, rant.getVehicle());
   }

   @Test
   public void shouldReturnAllRantsForRecentRants_TEMPORARY_UNTIL_I_FIGURE_OUT_SOMETHING_BETTER() {
      Rant rant1 = new Rant();
      rant1.setRantText("rant A");
      Rant rant2 = new Rant();
      rant2.setRantText("rant B");
      Rant rant3 = new Rant();
      rant3.setRantText("rant C");
      Rant rant4 = new Rant();
      rant4.setRantText("rant D");

      fakeRantDao.getAllRants(); // temporary
      expectLastCall().andReturn(asList(rant1, rant2, rant3, rant4));
      replay(fakeRantDao);

      List<Rant> rants = rantService.getRecentRants();

      assertEquals(4, rants.size());
      assertEquals("rant A", rants.get(0).getRantText());
      assertEquals("rant B", rants.get(1).getRantText());
      assertEquals("rant C", rants.get(2).getRantText());
      assertEquals("rant D", rants.get(3).getRantText());
   }

   @Test
   public void shouldReturnMotoristReturnedFromDao() {
      Motorist expectedMotorist = new Motorist();
      expectedMotorist.setUsername("already@exist.com");
      fakeRantDao.getMotoristByEmail("already@exist.com");
      expectLastCall().andReturn(expectedMotorist);
      replay(fakeRantDao);

      Motorist motorist = rantService.getMotoristByEmail("already@exist.com");
      assertNotNull(motorist);
      assertSame(expectedMotorist, motorist);
   }
}
