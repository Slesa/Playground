package de.slesa.jlo.ums.controllers;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import de.slesa.jlo.ums.model.User;

@Controller
@RequestMapping("/ums/users")
public class UserController {

	@Autowired
	IUserService userService;
	
	@RequestMapping(method=RequestMethod.GET)
	public ModelAndView listUsers() {
		Map<String, Object> model = new HashMap<String, Object>();
		model.put("users", userService.listUsers());
		return new ModelAndView("usersList.jsp", model);
	}
	
	@RequestMapping(value="/save", method=RequestMethod.POST)
	public ModelAndView saveUser(@ModelAttribute("user") User user, BindingResult result) {
		userService.addUser(user);
		return new ModelAndView("redirect:/forms/ums/users");
	}
	
	@RequestMapping(value="/add", method=RequestMethod.GET)
	public ModelAndView addUser(@ModelAttribute("user") User user, BindingResult result) {
		return new ModelAndView("userForm");
	}
}
