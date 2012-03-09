package de.slesa.jlo.pms.forms;

import javax.validation.constraints.Size;

import lombok.Getter;
import lombok.Setter;

import org.hibernate.validator.constraints.NotEmpty;

public class CurrencyForm {

	@NotEmpty
	@Size(min = 1, max = 50)
	@Getter @Setter
	private String currencyName;
}
