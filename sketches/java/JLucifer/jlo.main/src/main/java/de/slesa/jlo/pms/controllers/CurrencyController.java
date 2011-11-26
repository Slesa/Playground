package de.slesa.jlo.pms.controllers;

import java.util.Map;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import de.slesa.jlo.pms.forms.CurrencyForm;

// http://mhimu.wordpress.com/2007/11/27/spring-mvc-tutorial/

@Controller
@RequestMapping("/pms/currency.html")
public class CurrencyController {

	@SuppressWarnings({ "unchecked", "rawtypes" })
	@RequestMapping(method = RequestMethod.GET)
	public String showCurrencyForm(Map model) {
		CurrencyForm form = new CurrencyForm();
		model.put("currencyform", form);
		return "currencyform";
	}
}
