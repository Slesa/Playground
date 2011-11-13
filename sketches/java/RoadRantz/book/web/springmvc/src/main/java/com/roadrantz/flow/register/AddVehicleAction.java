package com.roadrantz.flow.register;

import org.springframework.webflow.execution.Action;
import org.springframework.webflow.execution.Event;
import org.springframework.webflow.execution.RequestContext;

import com.roadrantz.domain.Vehicle;

public class AddVehicleAction implements Action {

   public Event execute(RequestContext context) throws Exception {
      MotoristFormObject motoristForm = (MotoristFormObject) context
                        .getFlowScope().get("motoristForm");

      // Push a new blank vehicle on the list.
      motoristForm.getMotorist().getVehicles().add(new Vehicle());

      return new Event(this, "continue");
   }
}
