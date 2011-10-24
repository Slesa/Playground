package forms;

import javax.validation.constraints.Size;

import lombok.Getter;
import lombok.Setter;

import org.hibernate.validator.constraints.NotEmpty;

public class LoginForm {

	@NotEmpty
	@Size(min = 1, max = 50)
	@Getter @Setter
	private String userName;
	
	@NotEmpty
	@Size(min = 1, max = 20)
	@Getter @Setter
	private String password;
	
}
