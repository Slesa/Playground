package forms;

import javax.validation.constraints.Max;
import javax.validation.constraints.Min;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

import lombok.Getter;
import lombok.Setter;

import org.hibernate.validator.constraints.NotEmpty;
import org.springframework.format.annotation.NumberFormat;
import org.springframework.format.annotation.NumberFormat.Style;

public class ValidationForm {

	@NotEmpty
	@Size(min=1, max=20)
	@Getter @Setter
	private String userName;
	
	@NotNull
	@NumberFormat(style=Style.NUMBER)
	@Min(1)
	@Max(110)
	@Getter @Setter
	private Integer age;
	
	@NotEmpty(message="Password must not be blank.")
	@Size(min=1, max=10, message="Password must between 1 to 10")
	@Getter @Setter
	private String password;
	
	
}
