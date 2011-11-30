package de.slesa.jlo.pms.controllers;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import de.slesa.jlo.pms.model.Currency;

// http://mhimu.wordpress.com/2007/11/27/spring-mvc-tutorial/
// http://www.roseindia.net/tutorial/spring/spring3/web/spring-3-mvc-and-hibernate3-example-part3.html

@Controller
@RequestMapping("/pms/currencies")
public class CurrencyController {

	@Autowired
	private ICurrencyService currencyService;

	@RequestMapping(method = RequestMethod.GET)
	public ModelAndView listCurrencies() {
		Map<String, Object> model = new HashMap<String, Object>();
		model.put("currencies", currencyService.listCurrencies());
		return new ModelAndView("currenciesList.jsp", model);
	}
	
	@RequestMapping(value="/save", method=RequestMethod.POST)
	public ModelAndView saveCurrency(@ModelAttribute("currency") Currency currency, BindingResult result) {
		currencyService.addCurrency(currency);
		return new ModelAndView("redirect:/forms/pms/currencies.html");
	}
	
	@RequestMapping(value="/add", method=RequestMethod.GET)
	public ModelAndView addCurrency(@ModelAttribute("currency") Currency currency, BindingResult result) {
		return new ModelAndView("currencyForm");
	}
	
/*	public String showCurrencyForm(Map model) {
		CurrencyForm form = new CurrencyForm();
		model.put("currencyform", form);
		return "currencyform";
	}*/
	
}
