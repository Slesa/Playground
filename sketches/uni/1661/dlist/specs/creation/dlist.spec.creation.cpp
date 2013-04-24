#include <QtTest/QtTest>
#include "../../dlist.h"

class When_a_new_dlist_is_created: public QObject
{
	Q_OBJECT
	DList<int>* _subject;

private slots:
	void init() 
	{
		_subject = new DList<int>();
	}
	void cleanup()
	{
		delete _subject;
	}
	void it_should_have_no_succ()
	{
		QVERIFY(_subject->Succ()==NULL);
	}
	void it_should_have_no_pred()
	{
		QVERIFY(_subject->Pred()==NULL);
	}
	void it_should_return_itself_as_front() 
	{
		QVERIFY(_subject->Front()==_subject);
	}
};

QTEST_MAIN(When_a_new_dlist_is_created)
#include "dlist.specs.moc"
