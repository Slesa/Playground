package de.slesa.roadrantz.model;

import java.util.ArrayList;
import java.util.List;
import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.Transient;

import lombok.Data;
import lombok.EqualsAndHashCode;

@Entity
@Data
@EqualsAndHashCode(callSuper=true)
public class Motorist extends User {

	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private Long Id;
	
	private String email;
	
	@OneToMany(cascade=CascadeType.ALL, mappedBy="motorist")
	private List<Vehicle> vehicles;
	
	@Transient
	public List<Rant> getRants() {
		List<Rant> allRants = new ArrayList<Rant>();
		for(Vehicle vehicle : vehicles)
			allRants.addAll(vehicle.getRants());
		return allRants;
	}
	
	public Motorist() {
		vehicles = new ArrayList<Vehicle>();
	}
}
