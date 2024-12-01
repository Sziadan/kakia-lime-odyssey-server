using System.Runtime.InteropServices;

namespace kakia_lime_odyssey_packets.Packets.Models;

[StructLayout(LayoutKind.Sequential)]
public struct FPOS
{
	public float x;
	public float y;
	public float z;

	public FPOS CalculateDirection(FPOS destination)
	{
		double dx = destination.x - x;
		double dy = destination.y - y;
		double dz = 0; //destination.z - z;

		// Calculate the magnitude of the direction vector
		double magnitude = Math.Sqrt(dx * dx + dy * dy);

		// Normalize the direction vector
		return new FPOS
		{
			x = (float)(dx / magnitude),
			y = (float)(dy / magnitude),
			z = 0
		};
	}


	public double CalculateDistance(FPOS destination)
	{
		return Math.Sqrt(
			Math.Pow(destination.x - x, 2) +
			Math.Pow(destination.y - y, 2) +
			Math.Pow(destination.z - z, 2)
		);
	}

	public double CalculateTravelTime(FPOS destination, double velocity) 
	{ 
		double distance = CalculateDistance(destination); 
		return distance / velocity; 
	}

	public FPOS CalculateCurrentPosition(FPOS destination, double velocity, double elapsedTime, double epsilon = 0.0001)
	{
		FPOS direction = CalculateDirection(destination);
		double distanceTraveled = velocity * elapsedTime;

		double currentX = x + direction.x * distanceTraveled;
		double currentY = y + direction.y * distanceTraveled;
		double currentZ = z + direction.z * distanceTraveled;

		// Check if the new position is within epsilon distance of the destination
		if (Math.Abs(currentX - destination.x) < epsilon &&
			Math.Abs(currentY - destination.y) < epsilon &&
			Math.Abs(currentZ - destination.z) < epsilon)
		{
			return destination;
		}

		// Check if the new position exceeds the destination in any direction
		if ((direction.x >= 0 && currentX >= destination.x) || (direction.x < 0 && currentX <= destination.x) && 
			(direction.y >= 0 && currentY >= destination.y) || (direction.y < 0 && currentY <= destination.y)) 
		{ 
			return destination; 
		}

		return new FPOS()
		{
			x = (float)currentX,
			y = (float)currentY,
			z = (float)currentZ
		};
	}


	public FPOS GetRandomPositionWithinRadius(double radius)
	{
		Random random = new Random();

		double u = random.NextDouble();
		double v = random.NextDouble();
		double theta = 2 * Math.PI * u;
		double phi = Math.Acos(2 * v - 1);
		double r = radius * Math.Cbrt(random.NextDouble());
		double x = r * Math.Sin(phi) * Math.Cos(theta);
		double y = r * Math.Sin(phi) * Math.Sin(theta);
		//double z = r * Math.Cos(phi);

		return new FPOS()
		{
			x = (float)(this.x + x),
			y = (float)(this.y + y),
			z = this.z //(float)(this.z + z)
		};
	}

	public bool Compare(FPOS other, float epsilon = 0.0001f)
	{		
		return Math.Abs(x - other.x) < epsilon && 
			   Math.Abs(y - other.y) < epsilon && 
			   Math.Abs(z - other.z) < epsilon;
	}
}