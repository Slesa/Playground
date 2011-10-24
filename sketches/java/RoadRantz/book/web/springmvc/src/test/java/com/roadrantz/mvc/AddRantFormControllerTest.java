package com.roadrantz.mvc;

import static org.junit.Assert.*;
import junitx.framework.ArrayAssert;

import org.junit.Before;
import org.junit.Test;
import org.springframework.ui.ModelMap;

import com.roadrantz.domain.Rant;

public class AddRantFormControllerTest {
   private AddRantFormController controller;
   private FakeRantService fakeRantService;

   @Before
   public void setup() {
      controller = new AddRantFormController();
      fakeRantService = new FakeRantService();
      controller.rantService = fakeRantService;
   }

   @Test
   public void shouldShowRantForm() {
      assertEquals("addRant", controller.setupForm(new ModelMap()));
   }

   @Test
   public void shouldSetupBlankRantWithVehicleForFormBackingObject() {
      Rant rant = controller.setupRant();

      assertNotNull(rant);
      assertNotNull(rant.getVehicle());
   }

   @Test
   public void shouldListAllStates() {
      ArrayAssert.assertEquals(controller.getAllStates(),
                        WebConstants.ALL_STATES);
   }

   @Test
   public void shouldAddRant() {
      Rant rant = new Rant();
      String viewName = controller.addRant(rant);

      fakeRantService.addRant(rant);
      assertEquals("rantAdded", viewName);
   }
}
