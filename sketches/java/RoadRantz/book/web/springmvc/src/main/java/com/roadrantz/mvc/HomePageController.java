package com.roadrantz.mvc;

import java.util.Collections;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.roadrantz.domain.Rant;
import com.roadrantz.service.RantService;

@Controller
@RequestMapping("/home.htm")
public class HomePageController {
   @RequestMapping(method = RequestMethod.GET)
   public String showHomePage(ModelMap model) {

      List<Rant> rants = rantService.getRecentRants();
      Collections.reverse(rants);
      model.addAttribute(rants);

      return "home";
   }

   @Autowired
   RantService rantService;
}
