using System.Collections;

namespace Puzzle.NPath.Framework.CodeDom
{
	public class NPathSelectQuery : IValue
	{
		public NPathSelectClause Select;
		public NPathFromClause From;
		public NPathWhereClause Where;
		public NPathOrderByClause OrderBy;

		private Hashtable uniquePropertyPaths = new Hashtable();

		public IList GetReferencedPropertyPaths()
		{
			ArrayList references = new ArrayList(uniquePropertyPaths.Values);
			return references;
		}

//		public void AddPropertyPathReference(string propertyPath)
//		{
//			if (!(uniquePropertyPaths.ContainsKey(propertyPath)))
//			{
//				NPathPropertyPathReference reference = new NPathPropertyPathReference();
//				reference.PropertyPath = propertyPath;
//				NPathPropertyPathReferenceLocation referenceLocation;
//
//				//check if the path should be added in select or where clause
//				if (Where == null)
//					referenceLocation = NPathPropertyPathReferenceLocation.SelectClause;
//				else
//					referenceLocation = NPathPropertyPathReferenceLocation.WhereClause;
//
//				reference.ReferenceLocation = referenceLocation;
//				uniquePropertyPaths.Add(propertyPath,reference) ;
//			}
//		}

		public bool IsAggregate
		{
			get
			{
				foreach (NPathSelectField selectField in Select.SelectFields)
				{
					if (selectField.Expression is NPathFunction)
						return true;
				}
				return false;
			}
		}
	}
}