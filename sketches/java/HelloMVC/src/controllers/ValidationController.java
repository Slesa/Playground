package controllers;

import java.util.Map;

import javax.validation.Valid;

import model.User;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import forms.ValidationForm;

@Controller
@RequestMapping("/validationform.html")
@SuppressWarnings({ "rawtypes", "unchecked" })
public class ValidationController {

	// Display the form on the get request
	@RequestMapping(method = RequestMethod.GET)
	public String showValidationForm(Map model) {
		ValidationForm validationForm = new ValidationForm();
		model.put("validationForm", validationForm);
		return "validationform";
	}
	
	// Process the form.
	@RequestMapping(method = RequestMethod.POST)
	public String processValidationForm(@Valid ValidationForm validationForm, BindingResult result, Map model) {
		if (result.hasErrors()) {
			return "validationform";
		}
		// Add the saved validationForm to the model
		model.put("validationForm", validationForm);
		return "validationsuccess";
	}

}
