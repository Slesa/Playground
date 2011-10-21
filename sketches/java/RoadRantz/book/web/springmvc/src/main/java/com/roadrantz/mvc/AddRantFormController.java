package com.roadrantz.mvc;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.annotation.Secured;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;
import com.roadrantz.service.RantService;

@Controller
@Secured("ROLE_MOTORIST")
@RequestMapping("/addRant.htm")
public class AddRantFormController {

   @RequestMapping(method = RequestMethod.GET)
   public String setupForm(ModelMap model) {
      return "addRant";
   }

   @ModelAttribute("rant")
   public Rant setupRant() {
      Rant rant = new Rant();
      rant.setVehicle(new Vehicle());
      return rant;
   }

   @ModelAttribute("states")
   public String[] getAllStates() {
      return WebConstants.ALL_STATES;
   }

   @RequestMapping(method = RequestMethod.POST)
   protected String addRant(@ModelAttribute("rant")
   Rant rant) {
      rantService.addRant(rant);

      return "rantAdded";
   }

   @Autowired
   RantService rantService;
}
