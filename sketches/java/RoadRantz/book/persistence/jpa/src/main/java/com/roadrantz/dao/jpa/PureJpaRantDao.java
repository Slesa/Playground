package com.roadrantz.dao.jpa;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;

import org.joda.time.LocalDate;
import org.springframework.stereotype.Repository;

import com.roadrantz.dao.RantDao;
import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;

/**
 * RantDao implementation that only uses JPA. Contrast this to JpaRantDao which
 * extends JpaDaoSupport and uses JpaTemplate from Spring's API. The only tie to
 * Spring in PureJpaRantDao is the Repository annotation, which is used to guide
 * Spring in converting JPA-specific exceptions to subclasses of Spring's
 * platform-neutral DataAccessException.
 * 
 * The EntityManager is injected here by PersistenceAnnotationBeanPostProcessor,
 * configured in the Spring context along with the entity manager factory.
 * 
 * Upon looking back at the book, I discovered that I don't discuss this
 * implementation at all in the book. That's unfortunate, since the Pure JPA
 * approach is considered the preferred approach to using JPA in Spring.
 * Nevertheless, here's the implementation for your enjoyment.
 * 
 * @author wallsc
 */
@Repository
public class PureJpaRantDao implements RantDao {
   public static final String SELECT_RANTS_FOR_DAY = "select r from Rant r where r.postedDate=?1";
   public static final String SELECT_VEHICLE_BY_PLATE = "select v from Vehicle v where v.state=?1 and v.plateNumber=?2";
   public static final String SELECT_MOTORIST_BY_EMAIL = "select d from Motorist d where d.username=?1";
   public static final String SELECT_ALL_RANTS = "select r from Rant r";

   private EntityManager entityManager;

   @PersistenceContext
   public void setEntityManager(EntityManager entityManager) {
      this.entityManager = entityManager;
   }

   public void saveRant(Rant rant) {
      entityManager.persist(rant);
   }

   @SuppressWarnings("unchecked")
   public List<Rant> getAllRants() {
      return entityManager.createQuery(SELECT_ALL_RANTS).getResultList();
   }

   @SuppressWarnings("unchecked")
   public List<Rant> getRantsForDay(LocalDate day) {
      return entityManager.createQuery(SELECT_RANTS_FOR_DAY).setParameter(1,
                        day).getResultList();
   }

   public Vehicle findVehicleByPlate(String state, String plateNumber) {
      @SuppressWarnings("unchecked")
      List<Vehicle> vehicles = entityManager.createQuery(
                        SELECT_VEHICLE_BY_PLATE).setParameter(1, state)
                        .setParameter(2, plateNumber).getResultList();

      return vehicles.size() > 0 ? vehicles.get(0) : null;
   }

   public void saveVehicle(Vehicle vehicle) {
      entityManager.persist(vehicle);
   }

   public Motorist getMotoristByEmail(String email) {
      @SuppressWarnings("unchecked")
      List<Motorist> motorists = entityManager.createQuery(
                        SELECT_MOTORIST_BY_EMAIL).setParameter(1, email)
                        .getResultList();

      return motorists.size() > 0 ? motorists.get(0) : null;

   }

   public void saveMotorist(Motorist driver) {
      entityManager.persist(driver);
   }
}
