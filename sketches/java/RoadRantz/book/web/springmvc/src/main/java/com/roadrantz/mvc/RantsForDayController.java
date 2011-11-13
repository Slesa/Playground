package com.roadrantz.mvc;

import org.joda.time.LocalDate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.roadrantz.service.RantService;

@Controller
@RequestMapping("/rantsForDay.htm")
public class RantsForDayController {
   @RequestMapping(method = RequestMethod.GET)
   public String showRantsForDay(int month, int day, int year, ModelMap model) {
      LocalDate date = new LocalDate(year, month, day);
      model.addAttribute(rantService.getRantsForDay(date));

      return "dayRants";
   }

   @Autowired
   RantService rantService;
}
