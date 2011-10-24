package controllers;

import java.util.Map;

import javax.validation.Valid;

import lombok.Setter;
import model.Registration;
import model.RegistrationValidation;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller
@RequestMapping("/registrationform.html")
public class RegistrationController {
	
	@Autowired
	@Setter
	private RegistrationValidation registrationValidation;
	
	@RequestMapping(method = RequestMethod.GET)
	public String showRegistration(Map model) {
		Registration registration = new Registration();
		model.put("registration", registration);
		return "registrationform";
	}
	
	@RequestMapping(method = RequestMethod.POST)
	public String processRegistration(@Valid Registration registration, BindingResult result) {
		registrationValidation.validate(registration, result);
		if (result.hasErrors())
			return "registrationform";
		return "registrationsuccess";
	}
}
