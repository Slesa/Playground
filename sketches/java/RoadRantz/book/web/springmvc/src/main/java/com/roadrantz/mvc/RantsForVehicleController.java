package com.roadrantz.mvc;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.roadrantz.domain.Vehicle;
import com.roadrantz.service.RantService;

@Controller
@RequestMapping("/rantsForVehicle.htm")
public class RantsForVehicleController
{
    @SuppressWarnings("unchecked")
    @RequestMapping(method = RequestMethod.GET)
    public String showRantsForVehicle(Vehicle vehicle, ModelMap model)
    {
        model.addAttribute(rantService.getRantsForVehicle(vehicle));
        model.addAttribute(vehicle);

        return "vehicleRants";
    }

    @Autowired
    RantService rantService;
}
