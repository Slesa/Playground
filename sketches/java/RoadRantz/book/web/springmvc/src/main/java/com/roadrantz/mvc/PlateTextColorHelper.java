package com.roadrantz.mvc;

import java.util.HashMap;
import java.util.Map;

public class PlateTextColorHelper
{
    private static Map<String, String> STATE_TEXT_COLOR_MAP = new HashMap<String, String>();
    private static String DEFAULT_RGB = "#000000";

    static
    {
        STATE_TEXT_COLOR_MAP.put("de", "#FFFF66");
        STATE_TEXT_COLOR_MAP.put("tx", "#000066");
        STATE_TEXT_COLOR_MAP.put("ky", "#000066");
    }

    public static String getRGBForState(String state)
    {
        if (STATE_TEXT_COLOR_MAP.containsKey(state))
        {
            return STATE_TEXT_COLOR_MAP.get(state);
        }

        return DEFAULT_RGB;
    }
}
