//----------------------------------------------------------------------
// Isomorphic SmartClient
// Master-Detail load and save example (Hibernate)
//
// OrderItem Bean - to be managed by Hibernate
//----------------------------------------------------------------------

package com.isomorphic.examples.server.masterDetail;

import java.util.Date;
import java.io.Serializable;

public class OrderItem implements Serializable {

    // this bean has no business logic.  It simply stores data in these instance variables.
    protected Long pk;
    protected Order order;
    protected String itemDescription;
    protected float unitPrice;
    protected Long quantity;

    public OrderItem() { }

    // Getters
    public String getItemDescription() { return itemDescription; }
    public Order getOrder() { return order; }
    public Long getPk() { return pk; }
    public Long getQuantity() { return quantity; }
    public float getUnitPrice() { return unitPrice; }

    // Setters
    public void setItemDescription(String itemDescription) { this.itemDescription = itemDescription; }
    public void setOrder(Integer orderID) { this.order = order; }
    public void setPk(Long pk) { this.pk = pk; }
    public void setQuantity(Long quantity) { this.quantity = quantity; }
    public void setUnitPrice(float unitPrice) { this.unitPrice = unitPrice; }
    
}

