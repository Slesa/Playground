package com.roadrantz.domain;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

@Entity
@Table(name = "AUTHORITIES")
@SuppressWarnings("serial")
public class UserPrivilege implements Serializable {
   private Long id;
   private String username;
   private String privilege;
   private User user;

   public UserPrivilege() {
   }

   public UserPrivilege(String privilege) {
      this.privilege = privilege;
   }

   @Column(name = "AUTHORITY")
   public String getPrivilege() {
      return privilege;
   }

   public void setPrivilege(String privilege) {
      this.privilege = privilege;
   }

   public String getUsername() {
      return username;
   }

   public void setUsername(String username) {
      this.username = username;
   }

   public void setId(Long id) {
      this.id = id;
   }

   @Id
   @GeneratedValue(strategy = GenerationType.AUTO)
   public Long getId() {
      return id;
   }

   public void setUser(User user) {
      this.user = user;
   }

   @ManyToOne
   public User getUser() {
      return user;
   }
}
