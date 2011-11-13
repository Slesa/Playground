package com.roadrantz.mvc;

import static java.util.Arrays.*;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.joda.time.LocalDate;
import org.junit.Assert;

import com.roadrantz.domain.Motorist;
import com.roadrantz.domain.Rant;
import com.roadrantz.domain.Vehicle;
import com.roadrantz.service.MotoristAlreadyExistsException;
import com.roadrantz.service.RantService;

public class FakeRantService implements RantService
{
    private Rant rantReceived;
    private final List<Rant> testRants;

    public FakeRantService()
    {
        testRants = new ArrayList<Rant>();
        addRant(testRants, "Rant 1");
        addRant(testRants, "Rant 2");
        addRant(testRants, "Rant 3");
    }

    public void addMotorist(Motorist motorist) throws MotoristAlreadyExistsException
    {
        // TODO Auto-generated method stub
    }

    public void addRant(Rant rant)
    {
        rantReceived = rant;
    }

    public List<Rant> getRantsForDay(LocalDate date)
    {
        Rant rant1 = new Rant();
        Rant rant2 = new Rant();
        return asList(rant1, rant2);
    }

    public List<Rant> getRantsForVehicle(Vehicle vehicle)
    {
        Rant rant1 = new Rant();
        Rant rant2 = new Rant();
        return asList(rant1, rant2);
    }

    public List<Rant> getRecentRants()
    {
        return testRants;
    }

    public void assertThatRantWasAdded(Rant expectedRant)
    {
        Assert.assertSame(expectedRant, rantReceived);
    }

    private void addRant(List<Rant> rants, String text)
    {
        Rant rant = new Rant();
        rant.setRantText(text);
        rant.setPostedDate(new Date());
        rants.add(rant);
    }

    public Motorist getMotoristByEmail(String email)
    {
        // TODO Auto-generated method stub
        return null;
    }

    public List<Rant> getRantsForMotorist(String email)
    {
        // TODO Auto-generated method stub
        return null;
    }

}
