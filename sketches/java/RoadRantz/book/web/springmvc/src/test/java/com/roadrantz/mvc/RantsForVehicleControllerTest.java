package com.roadrantz.mvc;

import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.Test;
import org.springframework.ui.ModelMap;

import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;

public class RantsForVehicleControllerTest {
   RantsForVehicleController controller;

   @Before
   public void setup() {
      controller = new RantsForVehicleController();
      controller.rantService = new FakeRantService();
   }

   @Test
   @SuppressWarnings("unchecked")
   public void shouldDisplayRantsPageWithRantsForVehicle() {
      ModelMap model = new ModelMap();
      Vehicle vehicle = new Vehicle();
      assertEquals("vehicleRants", controller.showRantsForVehicle(vehicle,
                        model));

      assertNotNull(model.get("vehicle"));
      assertSame(vehicle, model.get("vehicle"));
      assertNotNull(model.get("rantList"));
      assertEquals(2, ((List<Rant>) model.get("rantList")).size());
   }
}
