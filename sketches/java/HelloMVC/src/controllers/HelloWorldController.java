package controllers;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class HelloWorldController {

	@RequestMapping("/helloworld")
	public ModelAndView helloWorld() {
		String message = "Hello World, Spring 3.0!";
		return new ModelAndView("helloworld", "message", message);
	}
}
