package com.roadrantz.domain;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "MOTORIST")
@SuppressWarnings("serial")
public class Motorist extends User implements Serializable {
   private Long id;
   private List<Vehicle> vehicles;

   public Motorist() {
      vehicles = new ArrayList<Vehicle>();
   }

   @OneToMany(cascade = CascadeType.ALL, mappedBy = "motorist")
   public List<Vehicle> getVehicles() {
      return vehicles;
   }

   public void setVehicles(List<Vehicle> vehicles) {
      this.vehicles = vehicles;
   }

   @Transient
   public List<Rant> getRants() {
      List<Rant> allRants = new ArrayList<Rant>();

      for (Vehicle vehicle : vehicles) {
         allRants.addAll(vehicle.getRants());
      }

      return allRants;
   }

   @Override
   public String toString() {
      return ToStringBuilder.reflectionToString(this);
   }

   @Id
   @GeneratedValue(strategy = GenerationType.AUTO)
   public Long getId() {
      return id;
   }

   public void setId(Long id) {
      this.id = id;
   }
}
