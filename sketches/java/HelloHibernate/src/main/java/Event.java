import java.util.Date;

public class Event {

	private Long id;
	private String title;
	private Date date;

	public Event() {
	}
	
	public Event(String title, Date date) {
		this.title = title;
		this.date = date;
	}
	
	public Long getId() {
		return id;
	}
	
	public void setId(Long id) {
		this.id = id;
	}
	
	public String getTitle() {
		return title;
	}
	
	public void setTitle(String title) {
		this.title = title;
	}
	
	public Date getDate() {
		return date;
	}
	
	public void setDate(Date date) {
		this.date = date;
	}
}
