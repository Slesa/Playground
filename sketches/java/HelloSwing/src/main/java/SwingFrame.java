import java.awt.BorderLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;


public class SwingFrame  extends JFrame implements ActionListener {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static final String[] MONTHS = {
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	};
	
	public SwingFrame() {
		super("Hello Swing!");
		
		JPanel namePanel = new JPanel();
		JLabel labelName = new JLabel("Name:", new ImageIcon("triblue.gif"), SwingConstants.LEFT);
		namePanel.add(labelName);
		
		JTextField textName = new JTextField(30);
		textName.setToolTipText("Enter your name");
		namePanel.add(textName);
		
		namePanel.setBorder(BorderFactory.createEtchedBorder());
		getContentPane().add(namePanel, BorderLayout.NORTH);
		
		JList listMonths = new JList(MONTHS);
		listMonths.setToolTipText("Select your month of birth");
		getContentPane().add(new JScrollPane(listMonths), BorderLayout.CENTER);
		
		JPanel buttonPanel = new JPanel();
		JButton buttonMetal = new JButton("Metal");
		buttonMetal.setToolTipText("Select Metal look&feel");
		buttonMetal.addActionListener(this);
		buttonPanel.add(buttonMetal);
		JButton buttonMotif = new JButton("Motif");
		buttonMotif.setToolTipText("Select Motif look&feel");
		buttonMotif.addActionListener(this);
		buttonPanel.add(buttonMotif);
		JButton buttonWindows = new JButton("Windows"); 
		buttonWindows.setToolTipText("Select windows look&feel");
		buttonWindows.addActionListener(this);
		buttonPanel.add(buttonWindows);
	
		buttonPanel.setBorder(BorderFactory.createEtchedBorder());
		getContentPane().add(buttonPanel, BorderLayout.SOUTH);
	}

	public void actionPerformed(ActionEvent event) {
		 
		String command = event.getActionCommand();
		
		String plaf = null;
		if (command.equals("Metal")) 
			plaf = "javax.swing.plaf.metal.MetalLookAndFeel";
		else if (command.equals("Motif"))
			plaf = "com.sun.java.swing.plaf.motif.MotifLookAndFeel";
		else if (command.equals("Windows"))
			plaf = "com.sun.java.swing.plaf.windows.WindowsLookAndFeel";
		if( plaf==null )
			return;
		try {
			UIManager.setLookAndFeel(plaf);
			SwingUtilities.updateComponentTreeUI(this);
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		} catch (InstantiationException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		} catch (UnsupportedLookAndFeelException e) {
			e.printStackTrace();
		}
	}
}
