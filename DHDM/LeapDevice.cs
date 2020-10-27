﻿//#define profiling
using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using LeapTools;
using Newtonsoft.Json;

namespace DHDM
{
	public class LeapDevice
	{
		LeapMotion leapMotion;
		bool testing;
		public bool Testing
		{
			get
			{
				return testing;
			}
			set
			{
				if (testing == value)
					return;
				testing = value;
				if (testing)
				{
					leapMotion = new LeapMotion();
					leapMotion.HandsMoved += LeapMotion_HandsMoved;
				}
				else
				{
					leapMotion.HandsMoved -= LeapMotion_HandsMoved;
					leapMotion.DisposeController();
					leapMotion = null;
				}
			}
		}

		SkeletalData2d skeletalData2d = new SkeletalData2d();
		bool ShowingDiagnostics()
		{
			return skeletalData2d.ShowingDiagnostics();
		}
		private void LeapMotion_HandsMoved(object sender, LeapFrameEventArgs ea)
		{
			if (!LeapCalibrator.Calibrated && !ShowingDiagnostics())
				return;

			if (LeapCalibrator.Calibrated)
				skeletalData2d.SetFromFrame(ea.Frame);
			else
				skeletalData2d.SetFromFrame(null);
			HubtasticBaseStation.UpdateSkeletalData(JsonConvert.SerializeObject(skeletalData2d));
		}

		public LeapDevice()
		{

		}

		public void ToggleTesting()
		{
			Testing = !Testing;
		}

		public void SetDiagnosticsOptions(bool showBackPlane, bool showFrontPlane, bool showActivePlane)
		{
			skeletalData2d.ShowActivePlane = showActivePlane;
			skeletalData2d.ShowFrontPlane = showFrontPlane;
			skeletalData2d.ShowBackPlane = showBackPlane;
		}

		public void CalibrationPointUpdated(LeapMotionCalibrationStep leapMotionCalibrationStep, Point position, DndCore.Vector point3D, double scale)
		{
			switch (leapMotionCalibrationStep)
			{
				case LeapMotionCalibrationStep.BackUpperLeft:
					skeletalData2d.BackPlane.UpperLeft2D = Point2D.From(position);
					skeletalData2d.BackPlane.UpperLeft = ScaledPoint.From(point3D, scale);
					break;
				case LeapMotionCalibrationStep.BackLowerRight:
					skeletalData2d.BackPlane.LowerRight2D = Point2D.From(position);
					skeletalData2d.BackPlane.LowerRight = ScaledPoint.From(point3D, scale);
					break;
				case LeapMotionCalibrationStep.FrontUpperLeft:
					skeletalData2d.FrontPlane.UpperLeft2D = Point2D.From(position);
					skeletalData2d.FrontPlane.UpperLeft = ScaledPoint.From(point3D, scale);
					break;
				case LeapMotionCalibrationStep.FrontLowerRight:
					skeletalData2d.FrontPlane.LowerRight2D = Point2D.From(position);
					skeletalData2d.FrontPlane.LowerRight = ScaledPoint.From(point3D, scale);
					break;
			}
		}
	}
}