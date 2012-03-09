package de.slesa.jlo.ums.forms;

import javax.validation.constraints.Size;

import lombok.Getter;
import lombok.Setter;

import org.hibernate.validator.constraints.NotEmpty;

public class UserForm {

	@NotEmpty
	@Size(min = 1, max = 50)
	@Getter @Setter
	private String userName;
}
