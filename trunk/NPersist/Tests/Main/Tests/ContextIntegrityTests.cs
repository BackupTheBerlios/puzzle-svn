using System;
using NUnit.Framework;
using Puzzle.NPersist.Framework;
using Puzzle.NPersist.Framework.Delegates;
using Puzzle.NPersist.Framework.Exceptions;
using Puzzle.NPersist.Framework.Interfaces;
using Puzzle.NPersist.Framework.Enumerations;
using Puzzle.NPersist.Framework.EventArguments;

namespace Puzzle.NPersist.Tests.Main
{
	[TestFixture()]
	public class ContextIntegrityTests : TestBase
	{
		private IContext m_Context;
		private IContext m_Context2;
		[TestFixtureSetUp()]
		public void SetupTestFixture()
		{
			m_Context = GetContext();
			m_Context2 = GetContext();
			m_Context.ExecutingSql += new ExecutingSqlEventHandler(this.m_Context_ExecutingSql) ;
		}

		[TestFixtureTearDown()]
		public void TearDownTestFixture()
		{
			m_Context.Dispose();
		}

		private void m_Context_ExecutingSql(object sender, SqlExecutorCancelEventArgs e)
		{
			Console.Out.WriteLine(e.Sql);
		}

		[Test()]
		[ExpectedException (typeof(NPersistException),"Object is not a NPersist managed object, do not use 'new' on Entities. (Property='Employee', Owner=SngTblWorkFolderAopProxy")]
		public void AssignUnmanagedObjectToProperty()
		{			
			SngTblWorkFolder wf = (SngTblWorkFolder) m_Context.CreateObject(typeof(SngTblWorkFolder));
			//this should fail
			wf.Employee = new SngTblEmployee() ;
		}

		[Test()]
		[ExpectedException (typeof(NPersistException),"Object is does not belong to the same IContext as the property owner. (Property='Employee', Owner=SngTblWorkFolderAopProxy")]
		public void AssignManagedObjectOfOtherContextToProperty()
		{			
			SngTblWorkFolder wf = (SngTblWorkFolder) m_Context.CreateObject(typeof(SngTblWorkFolder));
			SngTblEmployee e = (SngTblEmployee) m_Context2.CreateObject(typeof(SngTblEmployee));
			//this should fail
			wf.Employee = e ;
		}
	}
}
