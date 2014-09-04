﻿using System;
using System.Collections.Generic;

namespace Metrics.Core
{
    public sealed class NullMetricsRegistry : MetricsRegistry
    {
        private class NullCounter : Counter
        {
            public static readonly Counter Instance = new NullCounter();
            public void Increment() { }
            public void Increment(long value) { }
            public void Decrement() { }
            public void Decrement(long value) { }
        }

        private class NullMeter : Meter
        {
            public static readonly Meter Instance = new NullMeter();
            public void Mark() { }
            public void Mark(long count) { }
        }

        private class NullHistogram : Histogram
        {
            public static readonly Histogram Instance = new NullHistogram();
            public void Update(long value) { }
        }

        private class NullTimer : Timer
        {
            public static readonly Timer Instance = new NullTimer();
            public void Record(long time, TimeUnit unit) { }
            public void Time(Action action) { action(); }
            public T Time<T>(Func<T> action) { return action(); }
            public TimerContext NewContext() { return null; }
        }

        private class NullDataProvider : RegistryDataProvider
        {
            public static readonly NullDataProvider Instance = new NullDataProvider();
            public IEnumerable<GaugeValueSource> Gauges { get { yield break; } }
            public IEnumerable<CounterValueSource> Counters { get { yield break; } }
            public IEnumerable<MeterValueSource> Meters { get { yield break; } }
            public IEnumerable<HistogramValueSource> Histograms { get { yield break; } }
            public IEnumerable<TimerValueSource> Timers { get { yield break; } }
        }

        public string Name { get { return "NULL Registry"; } }
        public RegistryDataProvider DataProvider { get { return NullDataProvider.Instance; } }

        public void Gauge(string name, Func<MetricValueProvider<double>> valueProvider, Unit unit) { }
        public Counter Counter(string name, Unit unit) { return NullCounter.Instance; }
        public Meter Meter(string name, Unit unit, TimeUnit rateUnit) { return NullMeter.Instance; }
        public Histogram Histogram(string name, Unit unit, SamplingType samplingType) { return NullHistogram.Instance; }
        public Timer Timer(string name, Unit unit, SamplingType samplingType, TimeUnit rateUnit, TimeUnit durationUnit) { return NullTimer.Instance; }

        public void ClearAllMetrics() { }

    }
}
