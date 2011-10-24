package com.roadrantz.mvc;

import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.Test;
import org.springframework.ui.ModelMap;

import com.roadrantz.domain.Rant;

public class RantsForDayControllerTest {
   private RantsForDayController controller;

   @Before
   public void setup() {
      controller = new RantsForDayController();
      controller.rantService = new FakeRantService();
   }

   @Test
   @SuppressWarnings("unchecked")
   public void shouldDisplayRantsPageWithRantsForAGivenDay() {
      ModelMap model = new ModelMap();
      assertEquals("dayRants", controller.showRantsForDay(6, 9, 2007, model));
      List<Rant> rants = (List<Rant>) model.get("rantList");

      assertNotNull(rants);
      assertEquals(2, rants.size());
   }
}
