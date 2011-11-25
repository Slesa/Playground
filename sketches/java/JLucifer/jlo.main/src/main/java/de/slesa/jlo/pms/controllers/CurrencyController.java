package de.slesa.jlo.pms.controllers;

import java.util.Map;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import de.slesa.jlo.pms.forms.CurrencyForm;

@Controller
@RequestMapping("/currency.html")
public class CurrencyController {

	@SuppressWarnings({ "unchecked", "rawtypes" })
	@RequestMapping(method = RequestMethod.GET)
	public String showCurrencyForm(Map model) {
		CurrencyForm form = new CurrencyForm();
		model.put("currencyform", form);
		return "currencyform";
	}
}
