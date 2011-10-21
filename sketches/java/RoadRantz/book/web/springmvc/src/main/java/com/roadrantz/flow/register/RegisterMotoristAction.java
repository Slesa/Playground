package com.roadrantz.flow.register;

import org.springframework.webflow.execution.Action;
import org.springframework.webflow.execution.Event;
import org.springframework.webflow.execution.RequestContext;

import com.roadrantz.domain.Motorist;
import com.roadrantz.service.RantService;

public class RegisterMotoristAction implements Action {
   public Event execute(RequestContext context) throws Exception {
      MotoristFormObject motoristForm = (MotoristFormObject) context
                        .getFlowScope().get("motoristForm");
      Motorist motorist = motoristForm.getMotorist();
      // motorist.setVehicles(motoristForm.getScrubbedVehicles());
      motorist.getVehicles().remove(motorist.getVehicles().size() - 1);

      rantService.addMotorist(motorist);

      return new Event(this, "done");
   }

   private RantService rantService;

   public void setRantService(RantService rantService) {
      this.rantService = rantService;
   }
}
