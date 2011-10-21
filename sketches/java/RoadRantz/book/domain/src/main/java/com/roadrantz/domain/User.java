package com.roadrantz.domain;

import java.io.Serializable;
import java.util.HashSet;
import java.util.Set;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.Table;

@Entity
@Table(name = "USERS")
@SuppressWarnings("serial")
public class User implements Serializable {
   private Long id;
   private boolean enabled = true;
   private String firstName;
   private String lastName;
   private String username;
   private String password;
   private Set<UserPrivilege> privileges;

   public User() {
      privileges = new HashSet<UserPrivilege>();
   }

   @OneToMany(cascade = CascadeType.ALL, fetch = FetchType.LAZY, mappedBy = "user")
   public Set<UserPrivilege> getPrivileges() {
      return privileges;
   }

   public void setPrivileges(Set<UserPrivilege> privileges) {
      this.privileges = privileges;
   }

   @Id
   @GeneratedValue(strategy = GenerationType.AUTO)
   public Long getId() {
      return id;
   }

   public void setId(Long id) {
      this.id = id;
   }

   public String getFirstName() {
      return firstName;
   }

   public void setFirstName(String firstName) {
      this.firstName = firstName;
   }

   public String getLastName() {
      return lastName;
   }

   public void setLastName(String lastName) {
      this.lastName = lastName;
   }

   public String getUsername() {
      return username;
   }

   public void setUsername(String username) {
      this.username = username;
   }

   public String getPassword() {
      return password;
   }

   public void setPassword(String password) {
      this.password = password;
   }

   public void setEnabled(boolean enabled) {
      this.enabled = enabled;
   }

   public boolean isEnabled() {
      return enabled;
   }

}
