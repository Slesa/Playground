package com.roadrantz.domain;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class MotoristPrivilegeTest {
   @Test
   public void testConstructor() {
      UserPrivilege privilege = new UserPrivilege("privilege");

      assertEquals("privilege", privilege.getPrivilege());
   }

   @Test
   public void testSettersAndGetters() {
      UserPrivilege privilege = new UserPrivilege();
      privilege.setPrivilege("privy");
      privilege.setUsername("billy");

      assertEquals("privy", privilege.getPrivilege());
      assertEquals("billy", privilege.getUsername());
   }
}
