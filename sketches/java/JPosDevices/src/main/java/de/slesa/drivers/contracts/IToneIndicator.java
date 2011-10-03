package de.slesa.drivers.contracts;

public interface IToneIndicator extends IDevice {

	boolean canChangeVolume = false;
	boolean canChangeFrequency = false;
	boolean canChangePitch = false;
	
	int tone1Duration = 200;
	int tone1Volumne = 100;
	int tone1Pitch = 440;
	
	int tone2Duration = 200;
	int tone2Volumne = 100;
	int tone2Pitch = 660;
	
	int interToneWait = 100;
	
	void sound();
}
