package de.slesa.drivers.contracts;

public interface IDevice {

	/** Der Name des Ger�tes */
	String name = null;
	String description = null;
	String forTerminal = null;
	boolean enabled = false;
	
	void start();
	void stop();
	void select();
}
