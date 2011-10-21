package com.roadrantz.mvc;

import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.Test;
import org.springframework.ui.ModelMap;

import com.roadrantz.domain.Rant;

public class HomePageControllerTest
{
    private HomePageController controller;

    @Before
    public void setup()
    {
        controller = new HomePageController();
        controller.rantService = new FakeRantService();
    }

    @Test
    @SuppressWarnings("unchecked")
    public void shouldShowHomePageWithRecentRants()
    {
        ModelMap model = new ModelMap();
        assertEquals("home", controller.showHomePage(model));

        List<Rant> rants = (List<Rant>) model.get("rantList");

        assertNotNull(rants);
        assertEquals(3, rants.size());
        assertEquals("Rant 3", rants.get(0).getRantText());
        assertEquals("Rant 2", rants.get(1).getRantText());
        assertEquals("Rant 1", rants.get(2).getRantText());
    }
}
