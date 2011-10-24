package com.roadrantz.mvc;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.roadrantz.service.RantService;

@Controller
@RequestMapping("/rantsForMotorist.htm")
public class RantsForMotoristController
{
    @SuppressWarnings("unchecked")
    @RequestMapping(method = RequestMethod.GET)
    public String showRantsForVehicle(String email, ModelMap model)
    {
        model.addAttribute(rantService.getRantsForMotorist(email));

        return "vehicleRants";
    }

    @Autowired
    RantService rantService;
}
