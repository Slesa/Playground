package de.slesa.roadrantz.model;

import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;

import lombok.Data;

@Entity
@Data
public class Vehicle {

	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private Long Id;
	
	private String state;
	private String plateNumber;
	
	@ManyToOne
	private Motorist motorist;
	
	@OneToMany(targetEntity=Rant.class, cascade=CascadeType.ALL, mappedBy="vehicle")
	private List<Rant> rants;
	
	public void setPlateNumber(String plateNumber) {
		this.plateNumber = stripNonAlphanumeric(plateNumber);
	}
	
	private String stripNonAlphanumeric(String in) {
		if (in==null) return null;
		
		StringBuffer outBuffer = new StringBuffer(in.length());
		for (int i=0; i<in.length(); i++) {
			char c = in.charAt(i);
			if (Character.isLetter(c) || Character.isDigit(c)) {
				outBuffer.append(Character.toUpperCase(c));
			}
		}
		return outBuffer.toString();
	}
}
