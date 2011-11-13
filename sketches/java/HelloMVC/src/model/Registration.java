package model;

import javax.validation.constraints.Size;

import org.hibernate.validator.constraints.Email;
import org.hibernate.validator.constraints.NotEmpty;

import lombok.Getter;
import lombok.Setter;

public class Registration {

	@Getter @Setter
	private String userName;
	
	@NotEmpty
	@Size(min = 1, max = 20)
	@Getter @Setter
	private String password;
	
	@NotEmpty
	@Getter @Setter
	private String confirmPassword;
	
	@NotEmpty
	@Email
	@Getter @Setter
	private String email;
	
}
