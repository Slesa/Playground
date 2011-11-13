package de.slesa.roadrantz.model;

import java.util.Date;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.GeneratedValue;
import javax.persistence.ManyToOne;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import lombok.Data;

@Entity
@Data
public class Rant {

	@Id
	@GeneratedValue
	private Long Id;
	@ManyToOne
	private Vehicle vehicle;
	private String rantText;
	
	@Temporal(TemporalType.DATE)
	private Date postedDate;
	
}
