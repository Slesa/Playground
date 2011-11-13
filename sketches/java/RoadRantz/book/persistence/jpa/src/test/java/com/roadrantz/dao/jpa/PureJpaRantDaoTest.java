package com.roadrantz.dao.jpa;

import static com.roadrantz.dao.jpa.PureJpaRantDao.*;
import static java.util.Arrays.*;
import static java.util.Collections.*;
import static org.apache.commons.collections.CollectionUtils.*;
import static org.easymock.EasyMock.*;
import static org.junit.Assert.*;

import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.Query;

import org.joda.time.LocalDate;
import org.junit.Before;
import org.junit.Test;

import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;

public class PureJpaRantDaoTest
{
    private PureJpaRantDao dao;
    private EntityManager entityManager;

    @Before
    public void setup()
    {
        entityManager = createMock(EntityManager.class);

        dao = new PureJpaRantDao();
        dao.setEntityManager(entityManager);
    }

    @Test
    public void shouldSaveVehicle()
    {
        Vehicle vehicle = new Vehicle();
        entityManager.persist(vehicle);
        replay(entityManager);

        dao.saveVehicle(vehicle);
    }

    @Test
    public void shouldSaveMotorist()
    {
        Motorist motorist = new Motorist();
        entityManager.persist(motorist);
        replay(entityManager);

        dao.saveMotorist(motorist);
    }

    @Test
    public void shouldSaveRant()
    {
        Rant rant = new Rant();

        entityManager.persist(rant);
        replay(entityManager);

        dao.saveRant(rant);
    }

    @Test
    public void shouldReturnNullIfNoSingleMotoristFound()
    {
        String motoristEmail = "minnie.driver@roadhog.com";
        Query query = createMock(Query.class);
        expect(entityManager.createQuery(SELECT_MOTORIST_BY_EMAIL)).andReturn(query);
        expect(query.setParameter(1, motoristEmail)).andReturn(query);
        expect(query.getResultList()).andReturn(Collections.emptyList());
        replay(entityManager);
        replay(query);

        Motorist motorist = dao.getMotoristByEmail(motoristEmail);
        assertEquals(null, motorist);
    }

    @Test
    public void shouldReturnMotoristByEmail()
    {
        Motorist expectedMotorist = new Motorist();
        String motoristEmail = "minnie.driver@roadhog.com";
        Query query = createMock(Query.class);
        expect(entityManager.createQuery(SELECT_MOTORIST_BY_EMAIL)).andReturn(query);
        expect(query.setParameter(1, motoristEmail)).andReturn(query);
        expect(query.getResultList()).andReturn(Arrays.asList(expectedMotorist));
        replay(entityManager);
        replay(query);

        Motorist motorist = dao.getMotoristByEmail(motoristEmail);
        assertEquals(expectedMotorist, motorist);
    }

    @Test
    public void shouldReturnAllRants()
    {
        List<Rant> expectedRants = asList(new Rant(), new Rant(), new Rant(), new Rant());
        Query query = createMock(Query.class);

        expect(entityManager.createQuery(SELECT_ALL_RANTS)).andReturn(query);
        expect(query.getResultList()).andReturn(expectedRants);
        replay(entityManager);
        replay(query);

        assertTrue(isEqualCollection(expectedRants, dao.getAllRants()));
    }

    @Test
    public void shouldReturnVehicleForGivenPlate()
    {
        Vehicle vehicle = new Vehicle();
        Query query = createMock(Query.class);
        expect(entityManager.createQuery(SELECT_VEHICLE_BY_PLATE)).andReturn(query);
        expect(query.setParameter(1, "TX")).andReturn(query);
        expect(query.setParameter(2, "J55DNY")).andReturn(query);
        expect(query.getResultList()).andReturn(asList(vehicle));
        replay(entityManager);
        replay(query);

        assertEquals(vehicle, dao.findVehicleByPlate("TX", "J55DNY"));
    }

    @Test
    public void shouldReturnNullIfNoSingleVehicleIsFound()
    {
        Query query = createMock(Query.class);
        expect(entityManager.createQuery(SELECT_VEHICLE_BY_PLATE)).andReturn(query);
        expect(query.setParameter(1, "TX")).andReturn(query);
        expect(query.setParameter(2, "J55DNY")).andReturn(query);
        expect(query.getResultList()).andReturn(emptyList());
        replay(entityManager);
        replay(query);

        assertEquals(null, dao.findVehicleByPlate("TX", "J55DNY"));
    }

    @Test
    public void shouldReturnRantsForDay()
    {
        List<Rant> expectedRants = asList(new Rant(), new Rant(), new Rant(), new Rant());
        Query query = createMock(Query.class);

        expect(entityManager.createQuery(SELECT_RANTS_FOR_DAY)).andReturn(query);
        LocalDate day = new LocalDate();
        expect(query.setParameter(1, day)).andReturn(query);
        expect(query.getResultList()).andReturn(expectedRants);
        replay(entityManager);
        replay(query);

        assertTrue(isEqualCollection(expectedRants, dao.getRantsForDay(day)));
    }
}
