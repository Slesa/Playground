package de.slesa.jlo.ums.controllers;

import java.util.Map;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import de.slesa.jlo.ums.forms.UserForm;

@Controller
@RequestMapping("/user.html")
public class UserController {

	@SuppressWarnings({ "unchecked", "rawtypes" })
	@RequestMapping(method=RequestMethod.GET)
	public String showUserForm(Map model) {
		UserForm form = new UserForm();
		model.put("userform", form);
		return "userform";
	}
}
