package de.slesa.roadrantz.model;

import static org.junit.Assert.*;

import java.util.Arrays;
import java.util.List;

import org.junit.Test;

public class VehicleTest {

	@Test
	public void simpleSetterAndGettersShouldWork() {
		Vehicle vehicle = new Vehicle();
		vehicle.setId((long)321);
		Motorist expectedMotorist = new Motorist();
		vehicle.setMotorist(expectedMotorist);
		vehicle.setPlateNumber("ABC123");
		vehicle.setState("TX");
		List<Rant> expectedRants = Arrays.asList(new Rant(), new Rant(), new Rant());
		vehicle.setRants(expectedRants);
		
		assertEquals(new Long(321), vehicle.getId());
		assertEquals("ABC123", vehicle.getPlateNumber());
		assertEquals(expectedMotorist, vehicle.getMotorist());
		assertEquals(expectedRants, vehicle.getRants());
	}
	
	@Test
	public void shouldStripNonAlphanumericFromPlateNumber() {
		Vehicle vehicle = new Vehicle();
		vehicle.setPlateNumber(" 1.2/3&A$b!C'");
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
		vehicle1.setId((long)246);
		vehicle1.setState("TX");
		vehicle1.setPlateNumber("CBA123");
		
		Vehicle vehicle2 = new Vehicle();
		vehicle2.setId((long)246);
		vehicle2.setState("TX");
		vehicle2.setPlateNumber("CBA123");
		
		assertEquals(vehicle1, vehicle2);
	}
	
	@Test
	public void twoDifferentVehiclesShouldBeDifferent() {
		Vehicle vehicle1 = new Vehicle();
		vehicle1.setId((long)135);
		vehicle1.setState("AK");
		vehicle1.setPlateNumber("XYZ987");
		
		Vehicle vehicle2 = new Vehicle();
		vehicle2.setId((long)246);
		vehicle2.setState("TX");
		vehicle2.setPlateNumber("CBA123");
		
		assertFalse(vehicle1.equals(vehicle2));
	}
}
