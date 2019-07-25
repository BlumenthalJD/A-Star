using System.Collections.Generic;

namespace AStar
{
	public class Vertex
	{
		public Vertex(Vertex _parent = null, List<int> _position = null, int _hScore = 0, int _gScore = 0, int _fScore = 0)
		{
			parent = _parent;
			position = _position;
			hScore = _hScore;
			gScore = _gScore;
			fScore = _fScore;
		}

		public Vertex parent { get; set; }
		public List<int> position { get; set; }
		public double hScore { get; set; }
		public double gScore { get; set; }
		public double fScore { get; set; }
	}
}