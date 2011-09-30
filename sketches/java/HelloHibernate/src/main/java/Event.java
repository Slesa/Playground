import java.util.Date;

import lombok.Data;

public @Data class Event {

	private Long id;
	private String title;
	private Date date;

	public Event() {}
	
	public Event(String title, Date date) {
		this.title = title;
		this.date = date;
	}
	
}
