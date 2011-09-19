package Main;

public interface INumberFormatter {
	boolean can_handle(int number);
	String handle(int number);
}