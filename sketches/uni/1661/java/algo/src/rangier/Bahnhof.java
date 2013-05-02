package rangier;

import java.util.Stack;

public class Bahnhof {

	private Stack<WagonTarget> trackI;
	private Stack<WagonTarget> trackG;
	private Stack<WagonTarget> trackH;
	
	public Bahnhof() {
		trackI = new Stack<WagonTarget>();
		trackG = new Stack<WagonTarget>();
		trackH = new Stack<WagonTarget>();
	}
	
	public Stack<WagonTarget> getTrackI() {
		return this.trackI;
	}
	
	public Stack<WagonTarget> getTrackG() {
		return this.trackG;
	}
	
	public Stack<WagonTarget> getTrackH() {
		return this.trackH;
	}
	
	public int SwitchWagons() {
		int moves = moveWagonsToTrack(this.trackG) + moveWagonsToTrack(this.trackH);
		while(!this.trackI.isEmpty()) {
			WagonTarget target = this.trackI.pop();
			if(target==WagonTarget.A)
				trackG.push(target);
			if(target==WagonTarget.B)
				trackH.push(target);
			moves++;
		}
		return moves;
	}
	
	public Bahnhof AddWagonToG(WagonTarget target) {
		trackG.push(target);
		return this;
	}
	
	public Bahnhof AddWagonToH(WagonTarget target) {
		trackH.push(target);
		return this;
	}
	
	private int moveWagonsToTrack(Stack<WagonTarget> track) {
		int moves = 0;
		while(!track.isEmpty()) {
			WagonTarget target = track.pop();
			this.trackI.push(target);
			moves++;
		}
		return moves;
	}
	
}
